import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const ListCategories = Loadable(lazy(() => import('./PagingCategories')));

const categoryRoutes = [
  { path: '/category/paging', element: <ListCategories />, auth: authRoles.admin },
];

export default categoryRoutes;
