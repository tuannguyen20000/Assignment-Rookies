import { Stack } from '@mui/material';
import { Box, styled } from '@mui/system';
import { Breadcrumb, SimpleCard } from 'app/components';
import EditForm from './form/EditForm';

const Container = styled('div')(({ theme }) => ({
  margin: '30px',
  [theme.breakpoints.down('sm')]: { margin: '16px' },
  '& .breadcrumb': {
    marginBottom: '30px',
    [theme.breakpoints.down('sm')]: { marginBottom: '16px' },
  },
}));

const CategoryEditForm = () => {
  return (
    <Container>
      <Box className="breadcrumb">
        <Breadcrumb
          routeSegments={[{ name: 'Category', path: '/category/paging' }, { name: 'Edit' }]}
        />
      </Box>

      <Stack spacing={3}>
        <SimpleCard title="Edit">
          <EditForm />
        </SimpleCard>
      </Stack>
    </Container>
  );
};

export default CategoryEditForm;
