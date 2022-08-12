import {
  GET_LIST_CATEGORY,
  CREATE_CATEGORY,
  UPDATE_CATEGORY,
  SOFT_DELETE_CATEGORY,
  GET_BY_ID_CATEGORY,
  GET_CATEGORY,
} from '../actions/CategoryAction';

const initialState = {
  isCreated: false,
  isUpdated: false,
  isDeleted: false,
  pagingInfo: {},
  categoryList: [],
  category: [],
};

const CategoryReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_CATEGORY: {
      return {
        ...state,
        pagingInfo: action.payload,
        categoryList: [...action.payload.items],
      };
    }
    case GET_CATEGORY: {
      return {
        ...state,
        categoryList: [...action.payload],
      };
    }
    case CREATE_CATEGORY: {
      return {
        ...state,
        isCreated: true,
      };
    }
    case UPDATE_CATEGORY: {
      return {
        ...state,
        isUpdated: true,
      };
    }
    case SOFT_DELETE_CATEGORY: {
      return {
        ...state,
        isDeleted: true,
      };
    }
    case GET_BY_ID_CATEGORY: {
      return {
        ...state,
        category: [...action.payload],
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default CategoryReducer;
