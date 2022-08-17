import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const ListOrder = Loadable(lazy(() => import('./PagingOrder')));

const orderRoutes = [{ path: '/order/paging', element: <ListOrder />, auth: authRoles.admin }];

export default orderRoutes;
