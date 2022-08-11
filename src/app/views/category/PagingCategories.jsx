import {
  Box,
  IconButton,
  styled,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
} from "@mui/material";
import MaxHeightMenu from "../material-kit/menu/MaxHeightMenu";

import { useState, useEffect } from "react";
import { Breadcrumb, SimpleCard } from "app/components";
import { useDispatch, useSelector } from "react-redux";
import { baseUrlApi } from "app/utils/constant";

import { getListCategory } from "app/redux/actions/CategoryAction";

const Container = styled("div")(({ theme }) => ({
  margin: "30px",
  [theme.breakpoints.down("sm")]: { margin: "16px" },
  "& .breadcrumb": {
    marginBottom: "30px",
    [theme.breakpoints.down("sm")]: { marginBottom: "16px" },
  },
}));

const StyledTable = styled(Table)(() => ({
  whiteSpace: "pre",
  "& thead": {
    "& tr": { "& th": { paddingLeft: 0, paddingRight: 0 } },
  },
  "& tbody": {
    "& tr": { "& td": { paddingLeft: 0, textTransform: "capitalize" } },
  },
}));

const PaginationTable = () => {
  const currentParams = "category";
  const dispatch = useDispatch();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  useEffect(() => {
    dispatch(getListCategory());
  }, []);

  const { categoryList } = useSelector((state) => state.categories);

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
            <TableCell align="left">Category Name</TableCell>
            <TableCell align="center">Description</TableCell>
            <TableCell align="right">Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {categoryList.length > 0 &&
            categoryList
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((item, index) => (
                <TableRow key={index}>
                  <TableCell align="left">{item.categoryName}</TableCell>
                  <TableCell align="center">{item.description}</TableCell>
                  <Box
                    component="img"
                    align="center"
                    sx={{
                      height: 225,
                      width: 225,
                      maxHeight: { xs: 233, md: 167 },
                      maxWidth: { xs: 350, md: 250 },
                    }}
                    alt={item.productName}
                    src={baseUrlApi + item.thumbnailImage}
                  />
                  <TableCell align="right">
                    <IconButton>
                      <MaxHeightMenu
                        data={item.id}
                        current={currentParams}
                      ></MaxHeightMenu>
                    </IconButton>
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
        count={categoryList.length}
        onPageChange={handleChangePage}
        rowsPerPageOptions={[1, 5, 10, 25]}
        onRowsPerPageChange={handleChangeRowsPerPage}
        nextIconButtonProps={{ "aria-label": "Next Page" }}
        backIconButtonProps={{ "aria-label": "Previous Page" }}
      />
    </Box>
  );
};

const CategoryTable = () => {
  return (
    <Container>
      <Box className="breadcrumb">
        <Breadcrumb routeSegments={[{ name: "Category" }]} />
      </Box>

      <SimpleCard title="Category List">
        <PaginationTable />
      </SimpleCard>
    </Container>
  );
};

export default CategoryTable;
