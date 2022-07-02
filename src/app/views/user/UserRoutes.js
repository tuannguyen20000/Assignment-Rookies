import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const ListUsers = Loadable(lazy(() => import('./PagingUsers')));

const productRoutes = [{ path: '/user/paging', element: <ListUsers />, auth: authRoles.admin }];

export default productRoutes;
