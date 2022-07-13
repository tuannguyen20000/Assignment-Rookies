import {
  GET_LIST_PRODUCT,
  CREATE_PRODUCT,
  UPDATE_PRODUCT,
  SOFT_DELETE_PRODUCT,
  ASSIGN_TO_CATEGORY,
} from '../actions/ProductActions';

const initialState = {
  isCreated: false,
  isUpdated: false,
  isDeleted: false,
  isAssignToCategory: false,
  productList: [],
};

const ProductReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_PRODUCT: {
      return {
        productList: [...action.payload],
      };
    }
    case CREATE_PRODUCT: {
      return {
        ...state,
        isCreated: true,
      };
    }
    case UPDATE_PRODUCT: {
      return {
        ...state,
        isUpdated: true,
      };
    }
    case SOFT_DELETE_PRODUCT: {
      return {
        ...state,
        isDeleted: true,
      };
    }
    case ASSIGN_TO_CATEGORY: {
      return {
        ...state,
        isAssignToCategory: true,
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default ProductReducer;
