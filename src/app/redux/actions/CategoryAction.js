import axiosInstance, { axiosFormData } from '../../../axios.js';

export const GET_LIST_CATEGORY = 'GET_LIST_CATEGORY';
export const CREATE_CATEGORY = 'CREATE_CATEGORY';
export const UPDATE_CATEGORY = 'UPDATE_CATEGORY';
export const GET_BY_ID_CATEGORY = 'GET_BY_ID_CATEGORY';
export const SOFT_DELETE_CATEGORY = 'SOFT_DELETE_CATEGORY';
export const GET_CATEGORY = 'GET_CATEGORY';

export const getListCategory = (page, rowsPerPage) => (dispatch) => {
  axiosInstance
    .get(`Categories/get-paging-category?&pageIndex=${page + 1}&pageSize=${rowsPerPage}`)
    .then((res) => {
      dispatch({
        type: GET_LIST_CATEGORY,
        payload: res,
      });
    });
};

export const getCategory = () => (dispatch) => {
  axiosInstance.get(`Categories/get-list-category`).then((res) => {
    dispatch({
      type: GET_CATEGORY,
      payload: res,
    });
  });
};

export const createCategory = (formData) => (dispatch) => {
  axiosFormData.post('Categories/create-category', formData).then((res) => {
    dispatch({
      type: CREATE_CATEGORY,
      payload: res,
    });
  });
};

export const updateCategory = (id, formData) => (dispatch) => {
  console.log(id, formData);
  axiosFormData.put('Categories/update-category/' + id, formData).then((res) => {
    dispatch({
      type: UPDATE_CATEGORY,
      payload: res,
    });
  });
};

export const getByIdCategory = (Id) => (dispatch) => {
  axiosInstance.get('Categories/get-by-id/' + Id).then((res) => {
    dispatch({
      type: GET_BY_ID_CATEGORY,
      payload: res,
    });
  });
};

export const softDeleteCategory = (Id) => (dispatch) => {
  axiosInstance.post('Categories/soft-delete/' + Id).then((res) => {
    dispatch({
      type: SOFT_DELETE_CATEGORY,
      payload: res,
    });
  });
};
