import { useDispatch, useSelector } from 'react-redux';
import { updateProduct } from 'app/redux/actions/ProductActions';
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

  const { productList } = useSelector((state) => state.products);
  const currentProduct = productList.filter((product) => product.id == param.id);
  const { id, productName, description, price, thumbnailImages } = currentProduct[0];
  const [state, setState] = useState({
    ProductName: productName,
    Description: description,
    Price: price,
  });
  const [isFilePicked, setIsFilePicked] = useState(false);

  const formData = new FormData();
  formData.append('ProductName', state.ProductName);
  formData.append('Description', state.Description);
  formData.append('Price', state.Price);
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
    dispatch(updateProduct(id, formData));
    navigate('/product/paging');
  };

  const { ProductName, Description, Price } = state;

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
              alt={productName}
              src={baseUrlApi + thumbnailImages}
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
