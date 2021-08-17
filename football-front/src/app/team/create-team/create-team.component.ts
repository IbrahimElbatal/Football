import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { GeneralService } from 'src/app/services/general.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrls: ['./create-team.component.css']
})
export class CreateTeamComponent implements OnInit {

  createTeamFormGroup:FormGroup;
  nationalities$:Observable<any>;
  countries$:Observable<any>;
  
  logoFile:any;
  playerFiles:any =[];
  constructor(private fb:FormBuilder,
              private gService:GeneralService,
              private toastr:ToastrService,
              private snipper:NgxSpinnerService,
              private router:Router,
              private teamService:TeamService) { }

  ngOnInit(): void {
    this.countries$ = this.gService.countries();
    this.nationalities$ = this.gService.nationalites();
    
    this.createTeamFormGroup = this.fb.group({
      'Name':['',Validators.required],
      'CoachName':['',Validators.required],
      'Logo':['',Validators.required],
      'CountryId':[0,Validators.required],
      'FoundationDate':['',Validators.required],
      'Players': this.fb.array([
        this.fb.group({
          'Name':['',Validators.required],
          'DateOfBirth':['',Validators.required],
          'NationalityId':[0,Validators.required],
          'Image':['',Validators.required]
        })
      ])
    });

     
  }

  create(){
    if(this.createTeamFormGroup.invalid)
      return false;

    this.snipper.show();
  
    var team = this.createTeamFormGroup.value;
    var formData = new FormData();

    formData.append('Name',team.Name);
    formData.append('CountryId',team.CountryId);
    formData.append('CoachName',team.CoachName);
    formData.append('FoundationDate',team.FoundationDate);
    formData.append('Logo',this.logoFile);

    let players = team.Players;
    let teamPlayers = [];

    players.forEach(player => {
      teamPlayers.push({
        Name : player.Name,
        NationalityId :player.NationalityId,
        DateOfBirth:player.DateOfBirth
      });
      
    });
    formData.append('players',JSON.stringify(teamPlayers));
    
    this.playerFiles.forEach(element => {
      formData.append('PlayerImages',element)
    });

    this.teamService.create(formData)
    .subscribe(res =>{
      this.toastr.success('Team Created Successfully','Success');
      this.snipper.hide();
      this.router.navigateByUrl('team');
    },err =>{
      this.toastr.error('An error has been occured','Fail');
      this.snipper.hide();
    });
  }

  get playersArray(){
    return (<FormArray>this.createTeamFormGroup.get('Players'));
  }

  addGroupToArray(){
    this.playersArray.push(
      this.fb.group({
        'Name':['',Validators.required],
        'DateOfBirth':['',Validators.required],
        'NationalityId':[0,Validators.required],
        'Image':['',Validators.required]
      })
    );
  }

  removeFromArray(groupIndex:number){
    this.playersArray.removeAt(groupIndex);
  }
  onChange(event){
    this.logoFile= event.target.files[0];
  }
  onChangePlayerImage(event){
    this.playerFiles.push(event.target.files[0]);
  }

}
