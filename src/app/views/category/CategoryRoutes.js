import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const ListCategories = Loadable(lazy(() => import('./PagingCategories')));
const CreateCategory = Loadable(lazy(() => import('./CreateCategory')));
const EditCategory = Loadable(lazy(() => import('./EditCategory')));
const SoftDeleteCategory = Loadable(lazy(() => import('./SoftDeleteCategory')));

const categoryRoutes = [
  { path: '/category/paging', element: <ListCategories />, auth: authRoles.admin },
  { path: '/category/create', element: <CreateCategory />, auth: authRoles.admin },
  { path: '/category/edit/:id', element: <EditCategory />, auth: authRoles.admin },
  { path: '/category/delete/:id', element: <SoftDeleteCategory />, auth: authRoles.admin },
];

export default categoryRoutes;
