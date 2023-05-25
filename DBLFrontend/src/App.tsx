import * as React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AppHome } from "./components/AppHome";
import { AppMenu } from "./components/NavMenu";
import { Alert, AlertColor, Snackbar } from "@mui/material";
import { SnackbarContext } from "./components/SnackbarContext";
import { UserRegister } from "./components/users/UserRegister";
import { UserLogin } from "./components/users/UserLogin";
import { UserDelete } from "./components/users/UserDelete";
import { UserUpdate } from "./components/users/UserUpdate";
import { AllDestinations } from "./components/destinations/AllDestinations";
import { DestinationAdd } from "./components/destinations/DestinationAdd";
import { DestinationDetails } from "./components/destinations/DestinationDetails";
import { DestinationDelete } from "./components/destinations/DestinationDelete";
import { DestinationUpdate } from "./components/destinations/DestinationUpdate";
import { UserDetails } from "./components/users/UserDetails";
import { useState } from "react";

function App() {
  const [open, setOpen] = useState(false);
  const [severity, setSeverity] = useState<AlertColor>("success");
  const [message, setMessage] = useState("placeholder");

  const openSnackbar = (severity: AlertColor, message: string) => {
    handleClose();

    setTimeout(() => {
      setSeverity(severity);
      setMessage(message);
      setOpen(true);
    }, 250);
  };

  const handleClose = (
    event?: React.SyntheticEvent | Event,
    reason?: string
  ) => {
    if (reason === "clickaway") {
      return;
    }

    setOpen(false);
  };

  return (
    <React.Fragment>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity={severity}
          sx={{ width: "100%", whiteSpace: "pre-wrap" }}
        >
          {message}
        </Alert>
      </Snackbar>

      <SnackbarContext.Provider value={openSnackbar}></SnackbarContext.Provider>
      <AppMenu />

      <Routes>
        <Route path="/" element={<AppHome />} />

        <Route path="/users/register" element={<UserRegister />} />
        <Route path="/users/login" element={<UserLogin />} />

        <Route path="/users/:userId/details" element={<UserDetails />} />
        <Route path="/users/:userId/delete" element={<UserDelete />} />
        <Route path="/users/:userId/edit" element={<UserUpdate />} />

        <Route path="/destinations" element={<AllDestinations />} />
        <Route path="/destinations/add" element={<DestinationAdd />} />
        <Route
          path="/destinations/:destinationId/details"
          element={<DestinationDetails />}
        />
        <Route
          path="/destinations/:destinationId/delete"
          element={<DestinationDelete />}
        />
        <Route
          path="/destinations/:destinationId/edit"
          element={<DestinationUpdate />}
        />
        
        <Route path="/destinations/:userId" element={<AllDestinations />} />
      </Routes>
    </React.Fragment>
  );
}

export default App;
