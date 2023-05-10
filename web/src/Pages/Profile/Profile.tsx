import {Box} from "@mui/material";

import {ProfileForm} from "../../modules/Profile/ProfileForm";

import {useStyles} from "./styles";

export const Profile = () => {
  const styles = useStyles();
  return (
    <ProfileForm/>
  );
};

