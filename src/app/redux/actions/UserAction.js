import axiosInstance from '../../../axios.js';

export const GET_LIST_USER = 'GET_LIST_USER';
export const REGISTER_USER = 'REGISTER_USER';

export const getListUser = () => (dispatch) => {
  axiosInstance.get('Users/get-list-user').then((res) => {
    dispatch({
      type: GET_LIST_USER,
      payload: res,
    });
  });
};

export const registerUser = (Email, UserName, Password, ConfirmPassword) => (dispatch) => {
  axiosInstance
    .post('Authenticate/register', { Email, UserName, Password, ConfirmPassword })
    .then((res) => {
      dispatch({
        type: REGISTER_USER,
        payload: res,
      });
    });
};
