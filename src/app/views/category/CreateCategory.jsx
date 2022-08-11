import { Stack } from '@mui/material';
import { Box, styled } from '@mui/system';
import { Breadcrumb, SimpleCard } from 'app/components';
import CreateForm from './form/CreateForm';

const Container = styled('div')(({ theme }) => ({
  margin: '30px',
  [theme.breakpoints.down('sm')]: { margin: '16px' },
  '& .breadcrumb': {
    marginBottom: '30px',
    [theme.breakpoints.down('sm')]: { marginBottom: '16px' },
  },
}));

const CategoryCreateForm = () => {
  return (
    <Container>
      <Box className="breadcrumb">
        <Breadcrumb
          routeSegments={[{ name: 'Category', path: '/category/paging' }, { name: 'Create' }]}
        />
      </Box>

      <Stack spacing={3}>
        <SimpleCard title="Create Category">
          <CreateForm />
        </SimpleCard>
      </Stack>
    </Container>
  );
};

export default CategoryCreateForm;
