import { Box } from '@mui/material';
import Button from '@mui/material/Button';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Stepper from '@mui/material/Stepper';
import Typography from '@mui/material/Typography';
import React from 'react';
import CreateForm from './CreateForm';
import BadgeAutocomplete from '../../../views/material-kit/auto-complete/BadgeAutocomplete';

function getSteps() {
  return ['Infomation product', 'Add category', 'Review'];
}

function getStepContent(stepIndex) {
  switch (stepIndex) {
    case 0:
      return <CreateForm />;

    case 1:
      return <BadgeAutocomplete></BadgeAutocomplete>;

    case 2:
      return `In laoreet, dui vel tristique facilisis, velit dui dictum diam, nec feugiat mi mauris eu nunc. Nullam auctor eget ante ac laoreet. Aliquam et ante ligula. Nam imperdiet augue magna, ac tincidunt neque mollis nec. Sed eu nunc sit amet tellus commodo elementum non sit amet sem. Etiam ipsum nibh, rutrum vel ultrices in, vulputate ac dolor. Morbi dictum lectus id orci dapibus, et faucibus nulla viverra. Nulla consectetur ex vitae pretium vehicula. Quisque varius tempor erat et semper. Vivamus consectetur, eros sit amet ornare facilisis, nulla felis laoreet tortor, sit amet egestas risus ipsum sed eros.`;

    default:
      return `Aenean arcu ligula, porttitor id neque imperdiet, congue convallis erat. Integer libero sapien, convallis a vulputate vel, pretium vulputate metus. Donec leo justo, viverra ut tempor commodo, laoreet eu velit. Donec vel sem quis velit pharetra elementum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam in commodo mauris. Ut iaculis ipsum velit.`;
  }
}

export default function StepperForm() {
  const [activeStep, setActiveStep] = React.useState(0);
  const steps = getSteps();

  const handleNext = () => setActiveStep((prevActiveStep) => prevActiveStep + 1);

  const handleBack = () => setActiveStep((prevActiveStep) => prevActiveStep - 1);

  const handleReset = () => setActiveStep(0);

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
              Reset
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
                {activeStep === steps.length - 1 ? 'Finish' : 'Next'}
              </Button>
            </Box>
          </Box>
        )}
      </Box>
    </Box>
  );
}
