import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private titleService : Title, public router:Router){

  }


  ngOnInit(): void {
    console.log('app on int');
  }

  public setTitle(newTitle: string)
  {
    this.titleService.setTitle('Valhalla');
  }
}
