import { Box, Icon, IconButton, Menu, MenuItem } from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router-dom';

const options = ['edit', 'delete', 'detail'];

const ITEM_HEIGHT = 48;

function MaxHeightMenu(props) {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const navigate = useNavigate();
  const open = Boolean(anchorEl);

  function handleClick(event) {
    setAnchorEl(event.currentTarget);
  }

  function handleClose() {
    setAnchorEl(null);
  }

  function redirect(option) {
    navigate(`/product/${option}/${props.data}`);
  }

  return (
    <Box>
      <IconButton
        aria-label="More"
        aria-owns={open ? 'long-menu' : undefined}
        aria-haspopup="true"
        onClick={handleClick}
      >
        <Icon>more_vert</Icon>
      </IconButton>

      <Menu
        open={open}
        id="long-menu"
        anchorEl={anchorEl}
        onClose={handleClose}
        PaperProps={{ style: { maxHeight: ITEM_HEIGHT * 4.5, width: 200 } }}
      >
        {options.map((option) => (
          <MenuItem key={option} selected={option === 'Edit'} onClick={redirect.bind(this, option)}>
            {option}
          </MenuItem>
        ))}
      </Menu>
    </Box>
  );
}

export default MaxHeightMenu;
