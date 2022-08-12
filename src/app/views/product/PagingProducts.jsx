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
import IconButton from '@mui/material/IconButton';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import Collapse from '@mui/material/Collapse';
import Typography from '@mui/material/Typography';

import TableContainer from '@mui/material/TableContainer';
import Paper from '@mui/material/Paper';
import moment from 'moment';

import { useState, useEffect, Fragment } from 'react';
import { Breadcrumb, SimpleCard } from 'app/components';
import { useDispatch, useSelector } from 'react-redux';

import { getListProduct } from 'app/redux/actions/ProductActions';
import { baseUrlApi } from 'app/utils/constant';
import MaxHeightMenu from '../material-kit/menu/MaxHeightMenu';

const Container = styled('div')(({ theme }) => ({
  margin: '30px',
  [theme.breakpoints.down('sm')]: { margin: '16px' },
  '& .breadcrumb': {
    marginBottom: '30px',
    [theme.breakpoints.down('sm')]: { marginBottom: '16px' },
  },
}));

function Row(props) {
  const currentParams = 'product';

  const { row } = props;
  const [open, setOpen] = useState(false);

  return (
    <Fragment>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.productName}
        </TableCell>
        <TableCell align="center">{row.price}</TableCell>
        <TableCell align="center">{row.productQuantity}</TableCell>
        <TableCell align="center">{moment(row.createdDate).format('DD-MM-YYYY')}</TableCell>
        <TableCell align="center">{moment(row.updatedDate).format('DD-MM-YYYY')}</TableCell>
        <TableCell align="center">
          <Box
            component="img"
            align="center"
            sx={{
              height: '100%',
              width: '100%',
              maxHeight: { xs: 233, md: 167 },
              maxWidth: { xs: 350, md: 250 },
            }}
            alt={row.productName}
            src={baseUrlApi + row.thumbnailImage}
          />
        </TableCell>
        <TableCell align="right">
          <MaxHeightMenu data={row.id} current={currentParams}></MaxHeightMenu>
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={5}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Category
              </Typography>

              <Table size="small" aria-label="purchases">
                <TableBody>
                  <TableRow>
                    <TableCell component="th" scope="row">
                      {row.categories.map((item) => item + '   ')}
                    </TableCell>
                  </TableRow>
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </Fragment>
  );
}

const PaginationTable = () => {
  const dispatch = useDispatch();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  useEffect(() => {
    dispatch(getListProduct(page, rowsPerPage));
  }, [page, rowsPerPage]);

  const { productList } = useSelector((state) => state.products);

  const { pagingInfo } = useSelector((state) => state.products);

  return (
    <Box width="100%" overflow="auto">
      <TableContainer component={Paper}>
        <Table aria-label="collapsible table">
          <TableHead>
            <TableRow>
              <TableCell />
              <TableCell align="left">Name</TableCell>
              <TableCell align="center">Price</TableCell>
              <TableCell align="center">Quantity</TableCell>
              <TableCell align="center">Created Date</TableCell>
              <TableCell align="center">Updated Date</TableCell>
              <TableCell align="center"></TableCell>
              <TableCell align="right">Action</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {productList.length > 0 &&
              productList.map((item, index) => <Row key={index} row={item}></Row>)}
          </TableBody>
        </Table>

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
      </TableContainer>
    </Box>
  );
};

const ProductTable = () => {
  return (
    <Container>
      <Box className="breadcrumb">
        <Breadcrumb routeSegments={[{ name: 'Product' }]} />
      </Box>

      <SimpleCard title="Product List">
        <PaginationTable />
      </SimpleCard>
    </Container>
  );
};

export default ProductTable;
