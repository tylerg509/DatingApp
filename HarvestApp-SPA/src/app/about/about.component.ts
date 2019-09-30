import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  @Output() cancelAbout = new EventEmitter();
  aboutForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.createAboutForm();
  }

  createAboutForm() {
    this.aboutForm = this.fb.group({

    });
  }

  cancelAbouts() {
    this.cancelAbout.emit(false);
  }

}
