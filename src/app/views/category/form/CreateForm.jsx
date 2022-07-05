import { useDispatch } from 'react-redux';

import { createCategory } from 'app/redux/actions/CategoryAction';
import { Button, Grid, Icon, styled } from '@mui/material';
import { Span } from 'app/components/Typography';
import { TextValidator, ValidatorForm } from 'react-material-ui-form-validator';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const TextField = styled(TextValidator)(() => ({
  width: '100%',
  marginBottom: '16px',
}));

const CreateForm = () => {
  const [state, setState] = useState('');
  const [isFilePicked, setIsFilePicked] = useState(false);
  const navigate = useNavigate();
  const dispatch = useDispatch();

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
    dispatch(createCategory(formData));
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

export default CreateForm;
