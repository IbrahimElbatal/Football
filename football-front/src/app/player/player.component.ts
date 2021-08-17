import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { GeneralService } from '../services/general.service';
import { PlayerService } from '../services/player.service';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  players:any;
  teamId;
  nationalities$:Observable<any>;
  teams$:Observable<any>;
  modalRef:BsModalRef;
  editPlayerFormGroup:FormGroup;
  imageFile;
  playerId;

  constructor(private router:Router,
              private modalService:BsModalService,
              private fb:FormBuilder,
              public authService:AuthService,
              private route:ActivatedRoute,
              private toastr:ToastrService,
              private teamService:TeamService,
              private gService:GeneralService,
              private snipper:NgxSpinnerService,
              private playerService:PlayerService) { }

  ngOnInit(): void {
    this.teamId = +this.route.snapshot.paramMap.get('teamId');
     this.playerService.players(this.teamId)
      .subscribe(res => this.players = res);

      this.editPlayerFormGroup = this.fb.group({
        'Name':['',Validators.required],
        'DateOfBirth':['',Validators.required],
        'NationalityId':[0,Validators.required],
        'TeamId':['',Validators.required],
        'Image':['',Validators.required]
      });
  }

  deletePlayer(deletePlayerTemp:TemplateRef<any>,playerId){
    this.playerId = playerId;
    this.modalRef = this.modalService.show(deletePlayerTemp, {class: 'modal-sm'});

  }
  confirm(): void {
    this.snipper.show();
    this.playerService.delete(this.playerId)
      .subscribe(res =>{
        let index = this.players.findIndex(p =>p.id == this.playerId);
        this.players.splice(index,1);
        this.toastr.success('Player deleted successfully',"Success");
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
  openModal(editPlayer:TemplateRef<any>,playerId:number){
    this.nationalities$ = this.gService.nationalites();
    this.teams$ = this.teamService.teams();
    this.playerId = playerId;
    this.playerService.player(playerId)
      .subscribe(res =>{
        this.editPlayerFormGroup = this.fb.group({
          'Name':[res.name,Validators.required],
          'DateOfBirth':[res.dateOfBirth,Validators.required],
          'NationalityId':[res.nationalityId,Validators.required],
          'TeamId':[res.teamId,Validators.required],
          'Image':['',Validators.required]
        });
      });
    
    this.modalRef = this.modalService.show(editPlayer);
  }
  edit(){
    var player = this.editPlayerFormGroup.value;
    var formData = new FormData();

    formData.append('Id',this.playerId);
    formData.append('Name',player.Name);
    formData.append('NationalityId',player.NationalityId);
    formData.append('TeamId',player.TeamId);
    formData.append('DateOfBirth',player.DateOfBirth);
    formData.append('Image',this.imageFile);

    this.playerService.edit(formData)
    .subscribe((res:any) =>{
      this.toastr.success('Player Updated Successfully','Success');
      let index = this.players.findIndex(p =>p.id == res.id);
      this.players[index] = res;
      this.snipper.hide();
      this.modalRef.hide();
    },err =>{
      this.toastr.error('An error has been occured','Fail');
      this.snipper.hide();
    });
  }

  onChange(event){
    this.imageFile= event.target.files[0];
  }

}
