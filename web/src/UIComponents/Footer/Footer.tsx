import {FC} from "react";
import {Box, Link, Typography} from "@mui/material";

import {useStyles} from "./styles";

export const Footer: FC = () => {
  const styles = useStyles();
  return (
    <Box className={styles.footer}>
      <Typography>© 2023 practifly.com</Typography>
      <Link href="#" underline="none" className={styles.aboutUsLink}>Про нас</Link>
    </Box>
  );
};