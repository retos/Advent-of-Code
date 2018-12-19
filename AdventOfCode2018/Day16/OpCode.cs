namespace AdventOfCode2018.Day16
{
    public enum OpCode
    {
        //Addition:
        /// <summary>
        /// (add register) stores into register C the result of adding register A and register B.
        /// </summary>
        addr,
        /// <summary>
        /// (add immediate) stores into register C the result of adding register A and value B.
        /// </summary>
        addi,

        //Multiplication:
        /// <summary>
        /// (multiply register) stores into register C the result of multiplying register A and register B.
        /// </summary>
        mulr,
        /// <summary>
        /// (multiply immediate) stores into register C the result of multiplying register A and value B.
        /// </summary>
        muli,

        //Bitwise AND:
        /// <summary>
        /// (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
        /// </summary>
        banr,
        /// <summary>
        /// (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
        /// </summary>
        bani,
        
        //Bitwise OR:
        /// <summary>
        /// (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
        /// </summary>
        borr,
        /// <summary>
        /// (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
        /// </summary>
        bori,

        //Assignment:
        /// <summary>
        /// (set register) copies the contents of register A into register C. (Input B is ignored.)
        /// </summary>
        setr,
        /// <summary>
        /// (set immediate) stores value A into register C. (Input B is ignored.)
        /// </summary>
        seti,

        //Greater-than testing:
        /// <summary>
        /// (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
        /// </summary>
        gtir,
        /// <summary>
        /// (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
        /// </summary>
        gtri,
        /// <summary>
        /// (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
        /// </summary>
        gtrr,

        //Equality testing:
        /// <summary>
        /// (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
        /// </summary>
        eqir,
        /// <summary>
        /// (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
        /// </summary>
        eqri,
        /// <summary>
        /// (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.
        /// </summary>
        eqrr
    }
}