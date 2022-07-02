import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const ListProducts = Loadable(lazy(() => import('./PagingProducts')));

const productRoutes = [
  { path: '/product/paging', element: <ListProducts />, auth: authRoles.admin },
];

export default productRoutes;
