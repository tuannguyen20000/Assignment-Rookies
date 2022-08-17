export const navigations = [
  { label: 'User', type: 'label' },
  {
    name: 'Manage Users',
    icon: 'free_breakfast',
    children: [{ name: 'List Users', iconText: 'U', path: '/user/paging' }],
  },
  { label: 'Shop', type: 'label' },
  {
    name: 'Manage Products',
    icon: 'local_grocery_store',
    children: [
      { name: 'List Products', path: '/product/paging', iconText: 'P' },
      { name: 'Create Product', iconText: 'CP', path: '/product/create' },
    ],
  },
  {
    name: 'Manage Categories',
    icon: 'gradient',
    children: [
      { name: 'List Categories', path: '/category/paging', iconText: 'C' },
      { name: 'Create Category', iconText: 'CC', path: '/category/create' },
    ],
  },
  { label: 'Shipping', type: 'label' },
  {
    name: 'Manage Orders',
    icon: 'local_grocery_store',
    children: [{ name: 'List Orders', path: '/order/paging', iconText: 'O' }],
  },
];
