import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { GeneralService } from '../services/general.service';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {

  teams:any;
  editTeamFormGroup:FormGroup;
  modalRef:BsModalRef;
  countries$:Observable<any>;
  teamId;
  logoFile;
  constructor(private teamService:TeamService,
              private modalService:BsModalService,
              private gService:GeneralService,
              private fb:FormBuilder,
              public authService:AuthService,
              private toastr:ToastrService,
              private snipper:NgxSpinnerService) { }

  ngOnInit(): void {
     this.teamService.teams()
      .subscribe(res => this.teams = res);

      this.editTeamFormGroup = this.fb.group({
        'Name':['',Validators.required],
        'CoachName':['',Validators.required],
        'CountryId':[0,Validators.required],
        'Logo':['',Validators.required],
        'FoundationDate':['',Validators.required]

      });
  }

  deletePlayer(deleteTeam:TemplateRef<any>,teamId){
    this.teamId = teamId;
    this.modalRef = this.modalService.show(deleteTeam, {class: 'modal-sm'});

  }

  confirm(): void {
    this.snipper.show();

    this.teamService.delete(this.teamId)
      .subscribe(res =>{
        let index = this.teams.findIndex(p =>p.id == this.teamId);
        this.teams.splice(index,1);
        this.toastr.success('Team deleted successfully',"Success");
        this.snipper.hide();
      },err=>{
        this.toastr.error('An error has been occured',"Fail");
        this.snipper.hide();
      });
    this.modalRef.hide();
  }
 
  decline(): void {
    this.modalRef.hide();
  }
  openModal(editTeam:TemplateRef<any>,teamId:number){
    this.countries$ = this.gService.countries();
    this.teamId = teamId;
    this.teamService.team(teamId)
      .subscribe(res =>{
        this.editTeamFormGroup = this.fb.group({
          'Name':[res.name,Validators.required],
          'CoachName':[res.coachName,Validators.required],
          'CountryId':[res.countryId,Validators.required],
          'Logo':['',Validators.required],
          'FoundationDate':[res.foundationDate,Validators.required]
        });
      });
    
    this.modalRef = this.modalService.show(editTeam);
  }
  edit(){
    var team = this.editTeamFormGroup.value;
    var formData = new FormData();

    formData.append('Id',this.teamId);
    formData.append('Name',team.Name);
    formData.append('CountryId',team.CountryId);
    formData.append('CoachName',team.CoachName);
    formData.append('FoundationDate',team.FoundationDate);
    formData.append('Logo',this.logoFile);

    this.teamService.edit(formData)
    .subscribe((res:any) =>{
      this.toastr.success('Player Updated Successfully','Success');
      let index = this.teams.findIndex(p =>p.id == res.id);
      this.teams[index] = res;
      this.snipper.hide();
      this.modalRef.hide();
    },err =>{
      this.toastr.error('An error has been occured','Fail');
      this.snipper.hide();
    });
  }

  onChange(event){
    this.logoFile= event.target.files[0];
  }
}
