import {Box} from "@mui/material";

import {MyInput} from "../../UIComponents/Input/MyInput";

export const MaterialDetail = () => {
  return (
    <Box>
      <MyInput name="materialName" />
      <MyInput name="materialDescription" />
    </Box>
  );
};
