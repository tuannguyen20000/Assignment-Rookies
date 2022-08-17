import axiosInstance from '../../../axios.js';

export const GET_LIST_ORDER = 'GET_LIST_ORDER';

export const getListOrder = (page, rowsPerPage) => (dispatch) => {
  axiosInstance
    .get(`Orders/get-paging-order?&pageIndex=${page + 1}&pageSize=${rowsPerPage}`)
    .then((res) => {
      dispatch({
        type: GET_LIST_ORDER,
        payload: res,
      });
    });
};
