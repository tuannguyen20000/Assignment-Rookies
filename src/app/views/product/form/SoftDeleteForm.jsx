import { Box, Grid, styled } from '@mui/material';
import Button from '@mui/material/Button';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';

import Stepper from '@mui/material/Stepper';
import Typography from '@mui/material/Typography';
import React from 'react';
import { TextValidator, ValidatorForm } from 'react-material-ui-form-validator';

import { useParams, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { softDeleteProduct } from 'app/redux/actions/ProductActions';

import { baseUrlApi } from 'app/utils/constant';
const TextField = styled(TextValidator)(() => ({
  width: '100%',
  marginBottom: '16px',
}));

function DetailProduct() {
  const param = useParams();
  const { productList } = useSelector((state) => state.products);
  const currentProduct = productList.filter((product) => product.id == param.id);
  const { productName, description, price, thumbnailImage } = currentProduct[0];
  return (
    <div>
      <ValidatorForm>
        <Grid container spacing={6}>
          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField htmlFor="component-disabled" label="Product Name" value={productName} />

            <TextField htmlFor="component-disabled" label="Description" value={description} />

            <TextField htmlFor="component-disabled" label="Price" value={price} />
          </Grid>

          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
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
        </Grid>
      </ValidatorForm>
    </div>
  );
}

function getSteps() {
  return ['Infomation product', 'Confirm Delete'];
}

function getStepContent(stepIndex) {
  switch (stepIndex) {
    case 0:
      return <DetailProduct></DetailProduct>;

    case 1:
      return `Are you sure you want to delete this product?`;
  }
}

export default function SoftDeleteForm() {
  const [activeStep, setActiveStep] = React.useState(0);
  const steps = getSteps();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const param = useParams();

  const handleNext = () => {
    if (activeStep === steps.length - 1) {
      dispatch(softDeleteProduct(param.id));
    }
    setActiveStep((prevActiveStep) => prevActiveStep + 1);
  };

  const handleBack = () => setActiveStep((prevActiveStep) => prevActiveStep - 1);

  const handleReset = () => navigate('/product/paging');

  return (
    <Box>
      <Stepper activeStep={activeStep} alternativeLabel>
        {steps.map((label) => (
          <Step key={label}>
            <StepLabel>{label}</StepLabel>
          </Step>
        ))}
      </Stepper>

      <Box mt={4}>
        {activeStep === steps.length ? (
          <Box>
            <Typography>All steps completed</Typography>

            <Button sx={{ mt: 2 }} variant="contained" color="secondary" onClick={handleReset}>
              Go back to home
            </Button>
          </Box>
        ) : (
          <Box>
            <Typography>{getStepContent(activeStep)}</Typography>

            <Box pt={2}>
              <Button
                variant="contained"
                color="secondary"
                disabled={activeStep === 0}
                onClick={handleBack}
              >
                Back
              </Button>

              <Button sx={{ ml: 2 }} variant="contained" color="primary" onClick={handleNext}>
                {activeStep === steps.length - 1 ? 'Confirm' : 'Next'}
              </Button>
            </Box>
          </Box>
        )}
      </Box>
    </Box>
  );
}
