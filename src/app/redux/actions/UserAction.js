import axiosInstance from '../../../axios.js';

export const GET_LIST_USER = 'GET_LIST_USER';

export const getListUser = () => (dispatch) => {
  axiosInstance.get('Users/get-list-user').then((res) => {
    dispatch({
      type: GET_LIST_USER,
      payload: res,
    });
  });
};
