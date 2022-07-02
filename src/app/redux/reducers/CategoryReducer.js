import { GET_LIST_CATEGORY } from '../actions/CategoryAction';

const initialState = {
  categoryList: [],
};

const CategoryReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_LIST_CATEGORY: {
      return {
        categoryList: [...action.payload],
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
