import { GET_LIST_USER, REGISTER_USER } from '../actions/UserAction';

const initialState = {
  isRegistered: false,
  userList: [],
};

const UserReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_USER: {
      return {
        ...state,
        userList: [...action.payload],
      };
    }
    case REGISTER_USER: {
      return {
        ...state,
        isRegistered: true,
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
