import {
  Box,
  styled,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
} from '@mui/material';
import MaxHeightMenu from '../material-kit/menu/MaxHeightMenu';

import { useState, useEffect } from 'react';
import { Breadcrumb, SimpleCard } from 'app/components';
import { useDispatch, useSelector } from 'react-redux';

import { getListOrder } from 'app/redux/actions/OrderActions';

const Container = styled('div')(({ theme }) => ({
  margin: '30px',
  [theme.breakpoints.down('sm')]: { margin: '16px' },
  '& .breadcrumb': {
    marginBottom: '30px',
    [theme.breakpoints.down('sm')]: { marginBottom: '16px' },
  },
}));

const StyledTable = styled(Table)(() => ({
  whiteSpace: 'pre',
  '& thead': {
    '& tr': { '& th': { paddingLeft: 0, paddingRight: 0 } },
  },
  '& tbody': {
    '& tr': { '& td': { paddingLeft: 0, textTransform: 'capitalize' } },
  },
}));

const PaginationTable = () => {
  const currentParams = 'order';
  const dispatch = useDispatch();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  useEffect(() => {
    dispatch(getListOrder(page, rowsPerPage));
  }, [page, rowsPerPage]);

  const { orderList } = useSelector((state) => state.orders);

  const { pagingInfo } = useSelector((state) => state.orders);

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  return (
    <Box width="100%" overflow="auto">
      <StyledTable>
        <TableHead>
          <TableRow>
            <TableCell align="left">Ship Name</TableCell>
            <TableCell align="center">Ship Email</TableCell>
            <TableCell align="center">Ship Phone</TableCell>
            <TableCell align="center">Status</TableCell>
            <TableCell align="right">Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orderList.length > 0 &&
            orderList.map((item, index) => (
              <TableRow key={index}>
                <TableCell align="left">{item.shipName}</TableCell>
                <TableCell align="center">{item.shipEmail}</TableCell>
                <TableCell align="center">{item.shipPhoneNumber}</TableCell>
                <TableCell align="center">{item.status}</TableCell>
                <TableCell align="right">
                  <MaxHeightMenu data={item.id} current={currentParams}></MaxHeightMenu>
                </TableCell>
              </TableRow>
            ))}
        </TableBody>
      </StyledTable>

      <TablePagination
        sx={{ px: 2 }}
        page={page}
        component="div"
        rowsPerPage={rowsPerPage}
        count={pagingInfo.totalRecords}
        onPageChange={handleChangePage}
        rowsPerPageOptions={[1, 5, 10, 25]}
        onRowsPerPageChange={handleChangeRowsPerPage}
        nextIconButtonProps={{ 'aria-label': 'Next Page' }}
        backIconButtonProps={{ 'aria-label': 'Previous Page' }}
      />
    </Box>
  );
};

const OrderTable = () => {
  return (
    <Container>
      <Box className="breadcrumb">
        <Breadcrumb routeSegments={[{ name: 'Order' }]} />
      </Box>

      <SimpleCard title="Order List">
        <PaginationTable />
      </SimpleCard>
    </Container>
  );
};

export default OrderTable;
