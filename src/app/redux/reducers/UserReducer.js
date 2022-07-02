import { GET_LIST_USER } from '../actions/UserAction';

const initialState = {
  userList: [],
};

const UserReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_USER: {
      return {
        userList: [...action.payload],
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default UserReducer;
