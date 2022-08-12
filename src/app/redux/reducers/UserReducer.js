import { GET_LIST_USER, REGISTER_USER } from '../actions/UserAction';

const initialState = {
  isRegistered: false,
  pagingInfo: {},
  userList: [],
};

const UserReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_USER: {
      return {
        ...state,
        pagingInfo: action.payload,
        userList: [...action.payload.items],
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
