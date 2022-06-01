import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Offer } from 'src/app/models/offer';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
  @Input() offer: Offer | undefined;

  @Output() onDeleteClicked = new EventEmitter<Offer>();
  @Output() onEditClicked = new EventEmitter<Offer>();

  constructor() {}

  ngOnInit(): void {}

  deleteOffer() {
    this.onDeleteClicked.emit(this.offer);
  }

  editOffer() {
    this.onEditClicked.emit(this.offer);
  }
}
