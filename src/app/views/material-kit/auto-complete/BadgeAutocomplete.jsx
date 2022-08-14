import { Autocomplete, Chip, TextField } from '@mui/material';
import { Box, useTheme } from '@mui/system';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect } from 'react';
import { getCategory } from 'app/redux/actions/CategoryAction';

const BadgeAutocomplete = (props) => {
  const handleChange = (option, value) => {
    if (props.onUpdate != null) {
      props.onUpdatedCategory(value);
    } else {
      props.onSelectedCategory(value.map((category) => category.id));
    }
  };

  const handleGetCurrentCategory = () => {
    if (props.onUpdate != null) {
      const current = props.onUpdate.map((item) =>
        item.listItemCategory.map((category) => ({
          id: category.id,
          label: category.name,
        }))
      );
      return current[0];
    }
  };

  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(getCategory());
  }, [props.onUpdate]);

  const { categoryList } = useSelector((state) => state.categories);
  const theme = useTheme();

  return (
    <Box
      sx={{
        '& > * + *': {
          marginTop: theme.spacing(3),
        },
      }}
    >
      <Autocomplete
        multiple
        id="tags-filled"
        options={categoryList.map((option) => ({
          id: option.id,
          label: option.categoryName,
        }))}
        defaultValue={props.onUpdate != null ? handleGetCurrentCategory : []}
        getOptionLabel={(option) => option.label}
        onChange={handleChange}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        renderTags={(value, getTagProps) =>
          value.map((option, index) => (
            <Chip variant="outlined" label={option.label} {...getTagProps({ index })} />
          ))
        }
        renderInput={(params) => (
          <TextField
            {...params}
            variant="filled"
            label="Category"
            placeholder="Selected"
            fullWidth
          />
        )}
      />
    </Box>
  );
};

export default BadgeAutocomplete;
