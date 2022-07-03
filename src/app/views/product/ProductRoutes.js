import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const ListProducts = Loadable(lazy(() => import('./PagingProducts')));
const CreateProduct = Loadable(lazy(() => import('./CreateProduct')));

const productRoutes = [
  { path: '/product/paging', element: <ListProducts />, auth: authRoles.admin },
  { path: '/product/create', element: <CreateProduct />, auth: authRoles.admin },
];

export default productRoutes;
