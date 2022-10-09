import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { BasketService } from '../basket.service';

@Component({
  selector: 'app-basket-status',
  templateUrl: './basket-status.component.html',
  styleUrls: ['./basket-status.component.scss']
})
export class BasketStatusComponent implements OnInit {
  basketItemAddedSuscription: Subscription;
  basketUpdateSuscription: Subscription;
  authSubscription: Subscription;
  badge: number = 0;
  constructor(private basketService: BasketService, 
              private basketWrapperService: BasketWrapperService, 
              private authService: SecurityService, 
              private configurationService: ConfigurationService) { }


  ngOnInit(): void {
  }

}
