import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Offer } from '../models/offer';
import { ProductStore } from '../models/ProductStore';
import { ProductStoreId } from '../models/ProductStoreId';

@Injectable({
  providedIn: 'root',
})
export class OffersService {
  constructor(private httpClient: HttpClient) {}

  getOffers() {
    return this.httpClient.get<Offer[]>('https://localhost:44372/Offer/GetAll');
  }

  addOffer(offer: Offer) {

    const offerClone = Object.assign({}, offer, {

      productIds:

        offer.storeIds.length > 0 ? offer?.storeIds : offer.productIds,

    });
    console.log(offerClone);
    return this.httpClient.post(

      'https://localhost:44372/Offer/AddOffer',

      offerClone

    );

  }
  editOffer(offer: Offer) {

    const offerClone = Object.assign({}, offer, {

      productIds:

        offer.storeIds.length > 0 ? offer?.storeIds : offer.productIds,

    });
    console.log(offerClone);
    return this.httpClient.post(

      'https://localhost:44372/Offer/UpdateOffer',

      offerClone

    );
  }
  deleteOffer(offerId: number) {
    return this.httpClient.delete(
      `https://localhost:44372/Offer/DeleteOffer?offerId=${offerId}`
    );
  }

  // products service

  getProducts() {
    return this.httpClient.get<ProductStore[]>(
      'https://localhost:44372/Product/GetAllProducts'
    );
  }

  // stores service

  getStores() {
    return this.httpClient.get<ProductStore[]>(
      'https://localhost:44372/Store/GetAllStores'
    );
  }
}
