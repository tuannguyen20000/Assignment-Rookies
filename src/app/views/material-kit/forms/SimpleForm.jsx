import { useDispatch } from 'react-redux';

import { createProduct } from 'app/redux/actions/ProductActions';
import { Button, Grid, Icon, styled } from '@mui/material';
import { Span } from 'app/components/Typography';
import { TextValidator, ValidatorForm } from 'react-material-ui-form-validator';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const TextField = styled(TextValidator)(() => ({
  width: '100%',
  marginBottom: '16px',
}));

const SimpleForm = () => {
  const [state, setState] = useState('');
  const [isFilePicked, setIsFilePicked] = useState(false);
  const formData = new FormData();
  formData.append('ProductName', state.ProductName);
  formData.append('Description', state.Description);
  formData.append('Price', state.Price);
  formData.append('Status', state.Status);
  formData.append('ThumbnailImage', state.ThumbnailImage);
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const handleChange = (event) => {
    event.persist();
    setState({ ...state, [event.target.name]: event.target.value });
  };

  const handleFileUpdate = (event) => {
    setState({ ...state, [event.target.name]: event.target.files[0] });
    setIsFilePicked(true);
  };
  const handleSubmit = () => {
    dispatch(createProduct(formData));
    navigate('/product/paging');
  };

  const { ProductName, Description, Price, Status } = state;

  return (
    <div>
      <ValidatorForm onSubmit={handleSubmit} onError={(errors) => console.log(errors)}>
        <Grid container spacing={6}>
          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField
              type="text"
              name="ProductName"
              id="standard-basic"
              value={ProductName || ''}
              onChange={handleChange}
              errorMessages={['this field is required']}
              label="Product Name (Min length 4, Max length 9)"
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

            <TextField
              type="number"
              name="Price"
              label="Price"
              value={Price || ''}
              onChange={handleChange}
              validators={['required']}
              errorMessages={['this field is required']}
            />

            <TextField
              sx={{ mb: 4 }}
              type="number"
              name="Status"
              label="Status"
              onChange={handleChange}
              value={Status || ''}
              errorMessages={['this field is required']}
              validators={['required']}
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

export default SimpleForm;
