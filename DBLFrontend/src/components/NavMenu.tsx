import {
  Box,
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  Button,
  Menu,
  MenuItem,
  useTheme,
  useMediaQuery,
} from "@mui/material";

import { useContext, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { SnackbarContext } from "./SnackbarContext";
import { getAccount, logOut } from "../auth";
import { Role } from "../models/User";

import MenuIcon from "@mui/icons-material/Menu";
import HomeIcon from "@mui/icons-material/Home";
import AirplaneTicketIcon from "@mui/icons-material/AirplaneTicket";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import EmojiEmotionsIcon from "@mui/icons-material/EmojiEmotions";
import ShoppingBasketIcon from "@mui/icons-material/ShoppingBasket";
import ShieldIcon from "@mui/icons-material/Shield";
import LocalOfferIcon from "@mui/icons-material/LocalOffer";
import AccessTimeFilledIcon from "@mui/icons-material/AccessTimeFilled";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import AttachMoneyIcon from "@mui/icons-material/AttachMoney";
import GroupIcon from "@mui/icons-material/Group";

import LocalPoliceIcon from "@mui/icons-material/LocalPolice";
import LogoutIcon from "@mui/icons-material/Logout";

// TODO: fix button borders while resizing on lg
export const AppMenu = () => {
  const theme = useTheme();
  const isSmallScreen = useMediaQuery(theme.breakpoints.down(1260)); // lg
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const navigate = useNavigate();
  const openSnackbar = useContext(SnackbarContext);

  const location = useLocation();
  const path = location.pathname;

  const accountNameClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();

    const account = getAccount();
    if (account !== null) {
      navigate(`/users/${account.id}/details`);
    } else {
      navigate("/users/login");
    }
  };

  const logOutClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();

    logOut();
    navigate("/");
    openSnackbar("info", "Logged out successfully!");
  };

  const menuItems = [
    {
      link: "/destinations",
      title: "Destinations",
      icon: <AirplaneTicketIcon />,
    },
  ];

  return (
    <Box sx={{ flexGrow: 1, position: "sticky", top: "0", zIndex: "9" }}>
      <AppBar
        position="static"
        sx={{
          marginBottom: "20px",
          background: "linear-gradient(45deg, #8EA7E9, #7286D3)",
          padding: "0em 2em",
        }}
      >
        <Toolbar>
          {isSmallScreen ? (
            <>
              <IconButton
                size="large"
                edge="start"
                color="inherit"
                aria-label="menu"
                onClick={handleMenuOpen}
                sx={{ mr: 2 }}
              >
                <Typography
                  variant="h6"
                  component="div"
                  sx={{ margin: "auto", whiteSpace: "nowrap" }}
                >
                  DBucket
                </Typography>
              </IconButton>

              <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleMenuClose}
              >
                <MenuItem onClick={handleMenuClose} component={Link} to="/">
                  <IconButton size="large" color="inherit">
                    <HomeIcon />
                  </IconButton>
                  Home
                </MenuItem>

                {menuItems.map((item) => (
                  <MenuItem
                    key={item.title}
                    onClick={handleMenuClose}
                    component={Link}
                    to={item.link}
                  >
                    <IconButton size="large" color="inherit">
                      {item.icon}
                    </IconButton>
                    {item.title}
                  </MenuItem>
                ))}
              </Menu>
            </>
          ) : (
            <>
              <IconButton
                component={Link}
                to="/"
                size="large"
                edge="start"
                color="inherit"
                aria-label="school"
                sx={{ mr: 6, textDecoration: "none !important" }}
              >
                <Typography
                  variant="h6"
                  component="div"
                  sx={{ margin: "auto", whiteSpace: "nowrap" }}
                >
                  DBucket
                </Typography>
              </IconButton>

              {menuItems.map((item) => (
                <Button
                  key={item.title}
                  variant={path.startsWith(item.link) ? "outlined" : "text"}
                  to={item.link}
                  component={Link}
                  color="inherit"
                  sx={{ mr: 2, fontWeight: "light" }}
                  startIcon={item.icon}
                >
                  {item.title}
                </Button>
              ))}
            </>
          )}
          <Button
            component={Link}
            to={`/destinations/${getAccount()?.id}`}
            size="large"
            aria-label="userDestinations"
            color="inherit"
            sx={{
              mr: 0,
              fontWeight: "light",

              // display:
              //   getAccount()?.role === Role.user ? "inline-flex" : "none",
            }}
          >
            Your Orders
          </Button>
          <Box sx={{ display: "flex", marginLeft: "auto" }}>
            <Button
              variant="text"
              color="inherit"
              sx={{ mr: 2, fontWeight: "light" }}
              onClick={accountNameClick}
            >
              {getAccount()?.name ?? "Log In"}
            </Button>

            <Button
              variant="text"
              to="/users/register"
              component={Link}
              color="inherit"
              sx={{
                mr: 0,
                fontWeight: "light",
                display: getAccount() !== null ? "none" : "inline-flex",
              }}
            >
              Register
            </Button>

            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="school"
              sx={{
                mr: 0,
                display: getAccount() !== null ? "inline-flex" : "none",
              }}
              onClick={logOutClick}
            >
              <LogoutIcon />
            </IconButton>

            <IconButton
              component={Link}
              to={`/users/${getAccount()?.id}/details`}
              size="large"
              edge="start"
              color="inherit"
              aria-label="school"
              sx={{
                mr: 0,
                display: getAccount() !== null ? "inline-flex" : "none",
              }}
            >
              <AccountCircleIcon />
            </IconButton>
          </Box>
        </Toolbar>
      </AppBar>
    </Box>
  );
};
