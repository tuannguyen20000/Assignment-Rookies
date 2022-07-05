import axiosInstance, { axiosFormData } from '../../../axios.js';

export const GET_LIST_PRODUCT = 'GET_LIST_PRODUCT';
export const CREATE_PRODUCT = 'CREATE_PRODUCT';
export const GET_BY_ID_PRODUCT = 'GET_BY_ID_PRODUCT';
export const UPDATE_PRODUCT = 'UPDATE_PRODUCT';
export const SOFT_DELETE_PRODUCT = 'SOFT_DELETE_PRODUCT';

export const getListProduct = () => (dispatch) => {
  axiosInstance.get('Products/get-list-product').then((res) => {
    dispatch({
      type: GET_LIST_PRODUCT,
      payload: res,
    });
  });
};

export const createProduct = (formData) => (dispatch) => {
  axiosFormData.post('Products/create-product', formData).then((res) => {
    dispatch({
      type: CREATE_PRODUCT,
      payload: res,
    });
  });
};

export const getByIdProduct = (Id) => (dispatch) => {
  axiosInstance.get('Products/get-by-id' + Id).then((res) => {
    dispatch({
      type: GET_BY_ID_PRODUCT,
      payload: res.data,
    });
  });
};

export const updateProduct = (id, formData) => (dispatch) => {
  console.log(id, formData);
  axiosFormData.put('Products/update-product/' + id, formData).then((res) => {
    dispatch({
      type: 'UPDATE_PRODUCT',
      payload: res,
    });
  });
};

export const softDeleteProduct = (Id) => (dispatch) => {
  axiosInstance.post('Products/soft-delete/' + Id).then((res) => {
    dispatch({
      type: 'SOFT_DELETE_PRODUCT',
      payload: res,
    });
  });
};
