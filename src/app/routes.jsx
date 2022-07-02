import AuthGuard from 'app/auth/AuthGuard';
import chartsRoute from 'app/views/charts/ChartsRoute';
import dashboardRoutes from 'app/views/dashboard/DashboardRoutes';
import materialRoutes from 'app/views/material-kit/MaterialRoutes';
import NotFound from 'app/views/sessions/NotFound';
import sessionRoutes from 'app/views/sessions/SessionRoutes';
import { Navigate } from 'react-router-dom';
import MatxLayout from './components/MatxLayout/MatxLayout';

import productRoutes from './views/product/ProductRoutes';
import categoryRoutes from './views/category/CategoryRoutes';
import userRoutes from './views/user/UserRoutes';

const routes = [
  {
    element: (
      <AuthGuard>
        <MatxLayout />
      </AuthGuard>
    ),
    children: [
      ...dashboardRoutes,
      ...chartsRoute,
      ...materialRoutes,
      ...productRoutes,
      ...categoryRoutes,
      ...userRoutes,
    ],
  },
  ...sessionRoutes,
  { path: '/', element: <Navigate to="product/paging" /> },
  { path: '*', element: <NotFound /> },
];

export default routes;
