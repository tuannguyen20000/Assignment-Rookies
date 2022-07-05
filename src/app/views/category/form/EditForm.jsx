import { useDispatch, useSelector } from 'react-redux';
import { updateCategory } from 'app/redux/actions/CategoryAction';
import { Button, Grid, Icon, styled, Box } from '@mui/material';
import { Span } from 'app/components/Typography';
import { TextValidator, ValidatorForm } from 'react-material-ui-form-validator';
import { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { baseUrlApi } from 'app/utils/constant';

const TextField = styled(TextValidator)(() => ({
  width: '100%',
  marginBottom: '16px',
}));

const EditForm = () => {
  const param = useParams();
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const { categoryList } = useSelector((state) => state.categories);
  const currentCategory = categoryList.filter((category) => category.id == param.id);
  console.log(param.id);
  const { id, categoryName, description, thumbnailImage } = currentCategory[0];
  const [state, setState] = useState({
    CategoryName: categoryName,
    Description: description,
  });
  const [isFilePicked, setIsFilePicked] = useState(false);

  const formData = new FormData();
  formData.append('CategoryName', state.CategoryName);
  formData.append('Description', state.Description);
  formData.append('ThumbnailImage', state.ThumbnailImage);

  const handleChange = (event) => {
    event.persist();
    setState({ ...state, [event.target.name]: event.target.value });
  };

  const handleFileUpdate = (event) => {
    setState({ ...state, [event.target.name]: event.target.files[0] });
    setIsFilePicked(true);
  };
  const handleSubmit = () => {
    dispatch(updateCategory(id, formData));
    navigate('/category/paging');
  };

  const { CategoryName, Description } = state;

  return (
    <div>
      <ValidatorForm onSubmit={handleSubmit} onError={(errors) => console.log(errors)}>
        <Grid container spacing={6}>
          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField
              type="text"
              name="CategoryName"
              id="standard-basic"
              value={CategoryName || ''}
              onChange={handleChange}
              errorMessages={['this field is required']}
              label="Category Name (Min length 4, Max length 9)"
              validators={['required', 'minStringLength: 4', 'maxStringLength: 9']}
            />

            <TextField
              type="text"
              name="Description"
              label="Description"
              onChange={handleChange}
              value={Description || ''}
              validators={['required']}
              errorMessages={['this field is required']}
            />
          </Grid>

          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField type="file" name="ThumbnailImage" onChange={handleFileUpdate} />
            {isFilePicked ? (
              <div>
                <p>Filename: {state.ThumbnailImage.name}</p>
                <p>Filetype: {state.ThumbnailImage.type}</p>
                <p>Size in bytes: {state.ThumbnailImage.size}</p>
                <p>
                  Last modified date: {state.ThumbnailImage.lastModifiedDate.toLocaleDateString()}
                </p>
              </div>
            ) : (
              <p>Select a file to show details</p>
            )}
            <Box
              component="img"
              align="center"
              sx={{
                height: 225,
                width: 225,
                maxHeight: { xs: 233, md: 167 },
                maxWidth: { xs: 350, md: 250 },
              }}
              alt={categoryName}
              src={baseUrlApi + thumbnailImage}
            />
          </Grid>
        </Grid>
        <Button color="primary" variant="contained" type="submit">
          <Icon>send</Icon>
          <Span sx={{ pl: 1, textTransform: 'capitalize' }}>Submit</Span>
        </Button>
      </ValidatorForm>
    </div>
  );
};

export default EditForm;
