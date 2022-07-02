import axiosInstance from '../../../axios.js';

export const GET_LIST_CATEGORY = 'GET_LIST_CATEGORY';

export const getListCategory = () => (dispatch) => {
  axiosInstance.get('Categories/get-list-category').then((res) => {
    dispatch({
      type: GET_LIST_CATEGORY,
      payload: res,
    });
  });
};
