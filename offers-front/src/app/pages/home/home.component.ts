import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Offer } from 'src/app/models/offer';
import { ProductStore } from 'src/app/models/ProductStore';
import { OffersService } from 'src/app/services/offers.service';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  offers: Offer[] | undefined;
  products: ProductStore[] | any = [];
  stores: ProductStore[] | any = [];

  @ViewChild('openModalBtn')
  openModalBtn!: ElementRef;

  @ViewChild('closeModalBtn')
  closeModalBtn!: ElementRef;

  editedOffer: Offer | undefined;

  newOffer: Offer = {
    name: '',
    description: '',
    image: '',
    storeIds: [],
    productIds: [],
    storeName: '',
    startDate: '',
    expireDate: '',
    status: 0,
  };

  constructor(private offersService: OffersService) {}

  openAddEditModal() {
    this.editedOffer = undefined;

    this.newOffer = {
      name: '',
      description: '',
      image: '',
      storeIds: [],
      productIds: [],
      storeName: '',
      startDate: '',
      expireDate: '',
      status: 0,
    };

    this.openModalBtn.nativeElement.click();
  }

  closeModal() {
    this.closeModalBtn.nativeElement.click();
  }

  ngOnInit(): void {
    this.getOffers();
    this.getProducts();
    this.getStores();
  }

  getProducts() {
    this.offersService
      .getProducts()
      .subscribe((products) => (this.products = products));
  }

  getStores() {
    this.offersService
      .getStores()
      .subscribe((stores) => (this.stores = stores));
  }

  getOffers() {
    this.offersService.getOffers().subscribe((offers) => {
      this.offers = offers;
    });
  }

  submitAddEditModal() {
    !this.editedOffer ? this.addOffer() : this.editOffer();
  }
  uploadFile(event: any) {

    const file = event.target.files[0];

    const reader = new FileReader();

    reader.readAsDataURL(file);

    reader.onload = () => {

      this.newOffer.image = reader.result;

    }; 
  }

  editOffer() { 
    this.offersService.editOffer(this.newOffer).subscribe(
    (res) => {
      Swal.fire({
        icon: 'success',
        title: 'offer edited',
        text: 'offer edited successfully',
      });
      this.closeModal();
      this.getOffers();
    },
    (err) => {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
      });
    }
    
  );
  }

  addOffer() {
    this.offersService.addOffer(this.newOffer).subscribe(
      (res) => {
        Swal.fire({
          icon: 'success',
          title: 'offer added',
          text: 'offer added successfully',
        });
        this.closeModal();
        this.getOffers();
      },
      (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
      }
    );
  }

  deleteOffer(offer: Offer) {
    if(offer.id)
      this.offersService.deleteOffer(offer.id).subscribe(
        (res)=> {
          Swal.fire({
            icon: 'success',
            title: 'offer Deleted',
            text: 'offer Deleted successfully',
          });
          this.closeModal();
          this.getOffers();
        },
        (err) => {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
          });
        }
      );
  }

  onOfferClick(offer: any) {
    this.openAddEditModal();
    this.editedOffer = offer;
    this.newOffer = offer;
  }
  onSubmit(data: any) {
    
  }
}
