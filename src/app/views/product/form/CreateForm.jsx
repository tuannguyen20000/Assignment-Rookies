import { useDispatch } from "react-redux";
import { createProduct } from "app/redux/actions/ProductActions";
import { Button, Grid, Icon, styled, Typography } from "@mui/material";
import { Span } from "app/components/Typography";
import { TextValidator, ValidatorForm } from "react-material-ui-form-validator";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import BadgeAutocomplete from "../../../views/material-kit/auto-complete/BadgeAutocomplete";

const TextField = styled(TextValidator)(() => ({
  width: "100%",
  marginBottom: "16px",
}));

const CreateForm = () => {
  const [state, setState] = useState("");
  const [isFilePicked, setIsFilePicked] = useState(false);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [CategoriesId, setCategoriesId] = useState([]);

  const formData = new FormData();
  formData.append("ProductName", state.ProductName);
  formData.append("Description", state.Description);
  formData.append("Price", state.Price);
  formData.append("ThumbnailImage", state.ThumbnailImage);
  formData.append("ProductQuantity", state.ProductQuantity);
  if (CategoriesId != null) {
    CategoriesId.forEach((category) =>
      formData.append("CategoriesId[]", category)
    );
  }

  useEffect(() => {
    handleOnSelectedCategory();
    console.log(CategoriesId);
  }, []);
  const handleOnSelectedCategory = (inforCategory) => {
    setCategoriesId(inforCategory);
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
    dispatch(createProduct(formData));
    navigate("/product/paging");
  };

  const { ProductName, Description, Price, ProductQuantity } = state;

  return (
    <div>
      <ValidatorForm
        onSubmit={handleSubmit}
        onError={(errors) => console.log(errors)}
      >
        <Grid container spacing={6}>
          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField
              type="text"
              name="ProductName"
              id="standard-basic"
              value={ProductName || ""}
              onChange={handleChange}
              errorMessages={["this field is required"]}
              label="Product Name (Min length 4, Max length 9)"
              validators={[
                "required",
                "minStringLength: 4",
                "maxStringLength: 9",
              ]}
            />

            <TextField
              type="text"
              name="Description"
              label="Description"
              onChange={handleChange}
              value={Description || ""}
              validators={["required"]}
              errorMessages={["this field is required"]}
            />

            <TextField
              type="number"
              name="Price"
              label="Price"
              value={Price || ""}
              onChange={handleChange}
              validators={["required"]}
              errorMessages={["this field is required"]}
            />

            <TextField
              type="number"
              name="ProductQuantity"
              label="Quantity"
              value={ProductQuantity || ""}
              onChange={handleChange}
              validators={["required"]}
              errorMessages={["this field is required"]}
            />
          </Grid>

          <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
            <TextField
              type="file"
              name="ThumbnailImage"
              onChange={handleFileUpdate}
            />
            {isFilePicked ? (
              <div>
                <Typography>Filename: {state.ThumbnailImage.name}</Typography>
                <Typography>Filetype: {state.ThumbnailImage.type}</Typography>
                <Typography>
                  Size in bytes: {state.ThumbnailImage.size}
                </Typography>
                <Typography>
                  Last modified date:{" "}
                  {state.ThumbnailImage.lastModifiedDate.toLocaleDateString()}
                </Typography>
              </div>
            ) : (
              <div>
                <Typography>Select a file to show details</Typography>
              </div>
            )}
            <Grid item lg={6} md={6} sm={12} xs={12} sx={{ mt: 2 }}>
              <BadgeAutocomplete
                onSelectedCategory={handleOnSelectedCategory}
              ></BadgeAutocomplete>
            </Grid>
          </Grid>
        </Grid>
        <Button color="primary" variant="contained" type="submit">
          <Icon>send</Icon>
          <Span sx={{ pl: 1, textTransform: "capitalize" }}>Confirm</Span>
        </Button>
      </ValidatorForm>
    </div>
  );
};

export default CreateForm;
