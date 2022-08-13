import { useDispatch, useSelector } from 'react-redux';
import { updateProduct } from 'app/redux/actions/ProductActions';
import { Button, Grid, Icon, styled, Box, Typography } from '@mui/material';
import { Span } from 'app/components/Typography';
import { TextValidator, ValidatorForm } from 'react-material-ui-form-validator';
import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { baseUrlApi } from 'app/utils/constant';
import BadgeAutocomplete from '../../../views/material-kit/auto-complete/BadgeAutocomplete';

const TextField = styled(TextValidator)(() => ({
  width: '100%',
  marginBottom: '16px',
}));

const EditForm = () => {
  const param = useParams();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [CategoriesId, setCategoriesId] = useState([]);

  const { productList } = useSelector((state) => state.products);
  const currentProduct = productList.filter((product) => product.id == param.id);
  const { id, productName, description, price, thumbnailImage, productQuantity, listItemCategory } =
    currentProduct[0];
  const [state, setState] = useState({
    ProductName: productName,
    Description: description,
    Price: price,
    ProductQuantity: productQuantity,
  });
  const [isFilePicked, setIsFilePicked] = useState(false);

  const formData = new FormData();
  formData.append('ProductName', state.ProductName);
  formData.append('Description', state.Description);
  formData.append('Price', state.Price);
  formData.append('ThumbnailImage', state.ThumbnailImage);
  formData.append('ProductQuantity', state.ProductQuantity);
  if (CategoriesId != null) {
    CategoriesId.forEach((category) => formData.append('Categories[]', JSON.stringify(category)));
  }

  useEffect(() => {
    handleOnUpdatedCategory();
  }, []);

  const handleOnUpdatedCategory = (inforCategory) => {
    const updateCategories = [];
    const currentCategories = [];
    if (inforCategory != null) {
      inforCategory.map((category) => {
        updateCategories.push({
          id: category.id,
          name: category.label,
          selected: true,
        });
        listItemCategory.map((item, index) => {
          if (category.id === item.id) {
            currentCategories.push({
              id: item.id,
              name: item.name,
              selected: true,
            });
          }
          if (category.id !== item.id) {
            currentCategories.unshift({
              id: item.id,
              name: item.name,
              selected: false,
            });
          }
        });
      });
      const mapped = updateCategories.reduce((a, t) => ((a[t.id] = t), a), {});
      const mapped2 = currentCategories.reduce((a, t) => ((a[t.id] = t), a), {});
      const merge = Object.values({ ...mapped, ...mapped2 });
      setCategoriesId(merge);
    }
  };

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

  const { ProductName, Description, Price, ProductQuantity } = state;

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
              type="number"
              name="ProductQuantity"
              label="Quantity"
              value={ProductQuantity || ''}
              onChange={handleChange}
              validators={['required']}
              errorMessages={['this field is required']}
            />
          </Grid>

          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField type="file" name="ThumbnailImage" onChange={handleFileUpdate} />
            {isFilePicked ? (
              <div>
                <Typography>Filename: {state.ThumbnailImage.name}</Typography>
                <Typography>Filetype: {state.ThumbnailImage.type}</Typography>
                <Typography>Size in bytes: {state.ThumbnailImage.size}</Typography>
                <Typography>
                  Last modified date: {state.ThumbnailImage.lastModifiedDate.toLocaleDateString()}
                </Typography>
              </div>
            ) : (
              <Typography>Select a file to show details</Typography>
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
              src={baseUrlApi + thumbnailImage}
            />
          </Grid>
          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <BadgeAutocomplete
              onUpdate={currentProduct}
              onUpdatedCategory={handleOnUpdatedCategory}
            ></BadgeAutocomplete>
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
