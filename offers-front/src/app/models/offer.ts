import { Product } from './product';
import { ProductStoreId } from './ProductStoreId';
import { Store } from './store';

export interface Offer {
  id?: number;
  name: string;
  description: string;
  image: any;
  storeName: string;
  storeIds: ProductStoreId[];
  productIds: ProductStoreId[];
  startDate: string;
  expireDate: string;
  status: offerStatus;
}

export enum offerStatus{
  available , notAvilable
}
