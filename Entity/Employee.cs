using System;

namespace Entity
{
    public class Employee : BaseEntity
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }


        /*
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for t_employee
-- ----------------------------
DROP TABLE IF EXISTS `t_employee`;
CREATE TABLE `t_employee` (
`EmployeeId` int (11) DEFAULT NULL,
`FirstName` varchar(255) DEFAULT NULL,
`LastName` varchar(255) DEFAULT NULL,
`Salary` int (11) DEFAULT NULL,
`Id` int (11) NOT NULL,
`CreateTime` datetime DEFAULT NULL,
`UpdateTime` datetime DEFAULT NULL,
PRIMARY KEY(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_employee
-- ----------------------------
INSERT INTO `t_employee` VALUES('123123', '123', '123', '123', '1', '2017-05-31 17:38:13', '2017-05-31 17:38:16');
         */
    }
}
