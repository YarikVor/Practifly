import {CircularProgress} from "@mui/material";

import {ProfileForm} from "../../modules/ProfileForm/ProfileForm";
import {useAppSelector} from "../../hooks/hooks";
import {statusTypes} from "../../types/enums";

export const Profile = () => {
  const isFetching = useAppSelector((store) => store.user.status);


  return (
    <>
      <ProfileForm/>
    </>
  );
};

