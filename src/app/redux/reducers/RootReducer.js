import { combineReducers } from 'redux';
import EcommerceReducer from './EcommerceReducer';
import NavigationReducer from './NavigationReducer';
import NotificationReducer from './NotificationReducer';

import UserReducer from './UserReducer';
import CategoryReducer from './CategoryReducer';
import ProductReducer from './ProductReducer';
import OrderReducer from './OrderReducer';

const RootReducer = combineReducers({
  notifications: NotificationReducer,
  navigations: NavigationReducer,
  ecommerce: EcommerceReducer,
  products: ProductReducer,
  users: UserReducer,
  categories: CategoryReducer,
  orders: OrderReducer,
});

export default RootReducer;
