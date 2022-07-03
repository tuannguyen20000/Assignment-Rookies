import {
  GET_LIST_PRODUCT,
  CREATE_PRODUCT,
  GET_BY_ID_PRODUCT,
  UPDATE_PRODUCT,
  SOFT_DELETE_PRODUCT,
} from '../actions/ProductActions';

const initialState = {
  isCreated: false,
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
    case GET_BY_ID_PRODUCT: {
      return [...action.payload];
    }
    case UPDATE_PRODUCT: {
      return [...action.payload];
    }
    case SOFT_DELETE_PRODUCT: {
      return [...action.payload];
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default ProductReducer;
