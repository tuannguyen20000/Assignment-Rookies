import {
  GET_LIST_PRODUCT,
  CREATE_PRODUCT,
  UPDATE_PRODUCT,
  SOFT_DELETE_PRODUCT,
} from '../actions/ProductActions';

const initialState = {
  isCreated: false,
  isUpdated: false,
  isDeleted: false,
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
    default: {
      return {
        ...state,
      };
    }
  }
};

export default ProductReducer;
