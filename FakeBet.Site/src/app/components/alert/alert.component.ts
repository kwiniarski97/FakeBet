import {Component, OnInit} from '@angular/core';
import {AlertService} from '../../services/alert.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {

  constructor(private alertService: AlertService, private modalService: NgbModal) {
  }

  ngOnInit() {
  }

  emitError(title: string, message: string) {

  }

  open(content) {
    console.log(content);
    this.modalService.open(content);
  }
}



