import { GET_LIST_ORDER } from '../actions/OrderActions';

const initialState = {
  pagingInfo: {},
  orderList: [],
};

const OrderReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_ORDER: {
      return {
        ...state,
        pagingInfo: action.payload,
        orderList: [...action.payload.items],
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default OrderReducer;
