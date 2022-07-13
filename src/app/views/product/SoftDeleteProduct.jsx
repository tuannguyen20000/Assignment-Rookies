import { Stack } from '@mui/material';
import { Box, styled } from '@mui/system';
import { Breadcrumb, SimpleCard } from 'app/components';
import SoftDeleteForm from './form/SoftDeleteForm';

const Container = styled('div')(({ theme }) => ({
  margin: '30px',
  [theme.breakpoints.down('sm')]: { margin: '16px' },
  '& .breadcrumb': {
    marginBottom: '30px',
    [theme.breakpoints.down('sm')]: { marginBottom: '16px' },
  },
}));

const ProductSoftDeleteForm = () => {
  return (
    <Container>
      <Box className="breadcrumb">
        <Breadcrumb
          routeSegments={[{ name: 'Product', path: '/product/paging' }, { name: 'Delete' }]}
        />
      </Box>

      <Stack spacing={3}>
        <SimpleCard title="Delete Product">
          <SoftDeleteForm />
        </SimpleCard>
      </Stack>
    </Container>
  );
};

export default ProductSoftDeleteForm;
